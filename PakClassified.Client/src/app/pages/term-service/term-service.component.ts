import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-term-service',
  imports: [],
  templateUrl: './term-service.component.html',
  styleUrl: './term-service.component.css'
})
export class TermServiceComponent implements OnInit{
  ngOnInit(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
