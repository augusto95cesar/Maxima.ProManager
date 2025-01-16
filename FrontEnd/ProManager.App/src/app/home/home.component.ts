import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // Importe o CommonModule  
import { AuthService } from '../../core/service/authService';

@Component({
    selector: 'app-home',
    imports: [FormsModule, CommonModule],
    templateUrl: './home.component.html',
    styleUrl: './home.component.css'
})

export class HomeComponent {
    userLogado: string | null = "";
    constructor(private auth: AuthService, private router: Router) { }

    ngOnInit(): void {
        this.userLogado = this.auth.getUser();
    } 
}