import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit,signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App implements OnInit {
  
  private http = inject(HttpClient);
  protected title = 'DatingApp';
  protected members = signal <any>([]);
  

  async ngOnInit() {
    this.members.set(await this.getMembers())
  }

  async getMembers() {
    try{
      return lastValueFrom (this.http.get('https://localhost:5001/api/members'));
    } catch (error) {
      console.error(error);
      throw error;
    }
  }
}

