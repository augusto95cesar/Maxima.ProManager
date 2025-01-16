import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // Importe o CommonModule  
import { AuthService } from '../../../core/service/authService';
import { ProdutoService } from '../../../core/service/produtos.service';
import { Produtos } from '../../../core/models/products.models';
import { NgFor } from '@angular/common';
@Component({
  selector: 'app-produto',
  imports: [FormsModule, CommonModule, NgFor],
  templateUrl: './produto.component.html',
  styleUrl: './produto.component.css'
})

export class ProdutoComponent {
  dataSource: Produtos[] = []
  userLogado: string | null = "";
  constructor(private produtoService: ProdutoService, private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.loadDeProdutos();
    this.userLogado = this.auth.getUser();
  }

  loadDeProdutos() {
    this.produtoService.get().subscribe(
      (response) => {
        //console.log(response)
        this.dataSource = response;
      },
      (error) => {
        console.error('Erro ao obter dados:', error);
      }
    );
  }

  editProduct(codigo: string) {
    console.log("editar: " + codigo);
  }

  deleteProduct(codigo: string) {
    //console.log("editar: " + codigo);  
    this.produtoService.delete(codigo).subscribe(
      (response) => {
        this.loadDeProdutos();
      },
      (error) => {
        console.error('Erro ao obter dados:', error);
      }
    );
  }

  createProduto() {
    this.router.navigate(['/createproduto'])
  }

  produto() {
    this.router.navigate(['/produto'])
  }

  home() {
    this.router.navigate(['/home'])
  }

  relatorio() {
    alert("não existe relatorio cadastrado")
    this.router.navigate(['/home'])
  }

}