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

    find(searchText: string): Observable<Book[]> {
      return this._httpService.get<Book[]> ('api/book/' + searchText)
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
      });
    }

    edit(id: string) {
      this.editRowId = id;
    }

    search(searchText: string)
    {
      if (!searchText) {
            this.getAll();
            return;
      }
      this._bookService.find(searchText).subscribe(books => {
        this.books = books;
        console.log(books);
      });  
    }

    update(bookToUpdate: Book )
    {
      this.editRowId = '';
      console.log(bookToUpdate);
      this._bookService.update(bookToUpdate).subscribe(books => {
        this.books = books;
        console.log(books);
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
      });
        
    }

    delete(articleNumber: string)
    {
      if (!articleNumber) return;
      this._bookService.delete(articleNumber).subscribe(books => {
        this.books = books;
        console.log(books);
      });
    }
}


