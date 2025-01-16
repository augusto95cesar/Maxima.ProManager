import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; 
import { AuthService } from '../../../core/service/authService';
import { ProdutoService } from '../../../core/service/produtos.service';
import { Produtos } from '../../../core/models/products.models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 
import { DepartamentoService } from '../../../core/service/departamento,service';
import { Departamentos } from '../../../core/models/Departamentos.models';
import {  NgFor } from '@angular/common';
@Component({
    selector: 'app-create-produto',
    imports: [ReactiveFormsModule, FormsModule, CommonModule, NgFor],
    templateUrl: './create.produto.component.html',
    styleUrl: './create.produto.component.css'
})

export class CreateProdutoComponent {

    dataSource: Departamentos[] = []
    productForm: FormGroup;

   
    userLogado: string | null = "" ;
    constructor(private departamentoService: DepartamentoService, private produtoService: ProdutoService,private auth: AuthService, private router: Router,
      private fb: FormBuilder) {
        this.productForm = this.fb.group({
          codigo: ['', Validators.required],
          descricao: [''],
          preco: [null, [Validators.required, Validators.min(0.01)]],
          departamento: ['', Validators.required],
        });
      }

    ngOnInit(): void { 
      this.loadDeDepartamentos();
      this.userLogado = this.auth.getUser();
    }

    loadDeDepartamentos() {
      this.departamentoService.get().subscribe(
        (response) => {
          //console.log(response)
          this.dataSource = response; 
        },
        (error) => {
          console.error('Erro ao obter dados:', error); 
        }
      );
    }

    onSubmit(): void {
      if (this.productForm.valid) {
        //console.log('Formulário enviado com sucesso:', this.productForm.value);
        this.produtoService.post(this.productForm.value).subscribe({
          next: () => {
            this.router.navigate(['/produto'])
          },
          error: (err) => {
            //this.errorMessage = 'Erro ao fazer login. Verifique suas credenciais.';  
            console.log(err.error);
            alert(err.error[0])
            //this.router.navigate(['/'])
          }
        });
      }
    }
  
    onCancel(): void {
      this.router.navigate(['/home'])
    }

 }