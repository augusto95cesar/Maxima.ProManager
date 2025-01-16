import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // Importe o CommonModule  
import { AuthService } from '../../core/service/authService';

@Component({
    selector: 'app-produto',
    imports: [FormsModule, CommonModule],
    templateUrl: './produto.component.html',
    styleUrl: './produto.component.css'
})

export class ProdutoComponent { }