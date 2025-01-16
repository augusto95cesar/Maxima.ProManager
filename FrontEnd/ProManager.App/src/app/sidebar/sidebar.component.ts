import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // Importe o CommonModule   

@Component({
    selector: 'app-sidebar',
    imports: [FormsModule, CommonModule],
    templateUrl: './sidebar.component.html',
    styleUrl: './sidebar.component.css'
})

export class SidebarComponent { 
    constructor(private router: Router) { }

    createProduto(){
        this.router.navigate(['/createproduto'])
    }

    produto(){
        this.router.navigate(['/produto'])
    }

    home(){ 
        this.router.navigate(['/home'])
    }

    relatorio(){
        alert("não existe relatorio cadastrado")
        this.router.navigate(['/home'])
    }
}