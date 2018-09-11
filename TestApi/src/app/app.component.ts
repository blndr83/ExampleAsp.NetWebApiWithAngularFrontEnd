import { Component, Injectable } from '@angular/core';
import { OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { HttpClient } from '@angular/common/http'

  export interface Book {
    title: string;
    articleNumber: string;
    isLoaned: boolean;
  }


  @Injectable()  
  export class BookService {

    constructor(private _httpService: HttpClient) { }

    getAll(): Observable<Book[]> {
      return this._httpService.get<Book[]>('api/book')
    }

    add(book: Book): Observable<Book[]> {
      return this._httpService.post<Book[]>('api/book', book);
    }

    delete(articleNumber: string): Observable<Book[]> {
      return this._httpService.delete<Book[]>('api/book/' + articleNumber)
    }

    update(book: Book): Observable<Book[]> {
      return this._httpService.put<Book[]>('api/book', book)
    }
  }

@Component({
  providers: [BookService],
  moduleId: module.id,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})



export class AppComponent implements OnInit {
    title: string;
    editRowId: string = '';
    books: Book[] = [];
    displayedBooks: Book[] = [];
    searchText: string = '';
    showOnlyLoanedBooks: boolean = false;

    constructor(private _bookService: BookService) { }
    
      ngOnInit() {
        this.title = 'Books';
        this.getAll();
      }

    getAll() {
      this.editRowId = '';
      this._bookService.getAll().subscribe(books => {
        this.books = books;
        console.log(books);
        this.updateDisplayedBooks();
      });
    }

    edit(id: string) {
      this.editRowId = id;
    }

    updateDisplayedBooks() {
      console.log(this.searchText);
      console.log(this.showOnlyLoanedBooks);
      if (!this.searchText && this.showOnlyLoanedBooks === false) {
          this.displayedBooks = this.books;
          return;
      }
      if (!this.searchText && this.showOnlyLoanedBooks === true) {
        this.displayedBooks = this.books.filter(b => b.isLoaned === true);
        return;
      }

      let tempBooks = this.books.filter(b => b.articleNumber.toLowerCase().includes(this.searchText.toLowerCase())
        || b.title.toLowerCase().includes(this.searchText.toLowerCase()));

      if (this.showOnlyLoanedBooks === false) {
        this.displayedBooks = tempBooks;
      }
      else {
        this.displayedBooks = tempBooks.filter(b => b.isLoaned === true);
      }

    }

    search(searchText: string)
    {
      this.searchText = searchText;
      this.updateDisplayedBooks();
    }

    update(bookToUpdate: Book )
    {
      this.editRowId = '';
      console.log(bookToUpdate);
      this._bookService.update(bookToUpdate).subscribe(books => {
        this.books = books;
        console.log(books);
        this.updateDisplayedBooks();
      });
    }

    add(title: string, articleNumber: string)
    {
      if (!title || !articleNumber) return;
      if (title.trim() == '' || articleNumber.trim() == '') return;
      let newBook = <Book>{ articleNumber: articleNumber.trim(), title: title.trim(), isLoaned: false };
      this._bookService.add(newBook).subscribe(books => {
        this.books = books;
        console.log(books);
        this.updateDisplayedBooks();
      });
        
    }

    delete(articleNumber: string)
    {
      if (!articleNumber) return;
      this._bookService.delete(articleNumber).subscribe(books => {
        this.books = books;
        console.log(books);
        this.updateDisplayedBooks();
      });
    }
}


