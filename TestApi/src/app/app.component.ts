import { Component } from '@angular/core';
import { Http, RequestOptions} from '@angular/http';
import { OnInit } from '@angular/core';

@Component({
  moduleId: module.id,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title: string;
    books: Book[];

    constructor(private _httpService: Http) { }
    
    ngOnInit() {
        this.title = 'Books';
        this.getAll();
    }

    private getAll() {
        this._httpService.get('api/book').subscribe(books => {
            this.books = books.json();
            console.log(this.books);
        });
    }

    search(searchText: string)
    {
      if (!searchText) {
            this.getAll();
            return;
        }

      this._httpService.get('api/book/' + searchText).subscribe(books => {
            this.books = books.json();
            console.log(this.books);
        })
    }

    add(name: string, articleNumber: string)
    {
      if (!name || !articleNumber) return;
      if (name.trim() == '' || articleNumber.trim() == '') return;
      let newBook = <Book>{ ArticleNumber: articleNumber.trim(), Name: name.trim() };
        let header = new Headers();
        header.append('Content-Type', 'application/json')
        this._httpService.post('api/book', newBook, header).subscribe(books => {
            this.books = books.json();
            console.log(this.books);
        });
        
    }

    delete(articleNumber: string)
    {
        if (!articleNumber) return;

        this._httpService.delete('api/book/' + articleNumber).subscribe(books => {
            this.books = books.json();
        });
    }
}

 interface Book
{
    Name: string;
    ArticleNumber: string;
}
