import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../core/service/authService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  imports: [FormsModule]
})

export class LoginComponent implements OnInit {

  username: string = '';
  password: string = '';
  
 
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() { this.authService.logout() }

  onLogin() {
    if (this.username != "" && this.password != "") {
      this.authService.login(this.username, this.password).subscribe({
        next: () => {
          this.router.navigate(['/home']); // Navegar para a página principal após o login  
        },
        error: (err) => {
          console.log(err)
          //this.errorMessage = 'Erro ao fazer login. Verifique suas credenciais.';  
        }
      });
    }
  }

  navigateToRegister() {
    alert("não existe a pagina de cadastro!")
    // if(this.username === '' || this.password ===''){
    //   alert('preencha os campos de login e senha!');
    // }else{
    //   const observer = {
    //     next: (response: any) => {
    //       alert('Login cadastrado com sucesso!'); 
    //     },
    //     error: (err: any) => {
    //       const errorMessage = err?.error?.[0]?.code || 'Erro ao fazer login. Verifique suas credenciais.';
    //       alert(errorMessage);
    //     },
    //     complete: () => {
    //       console.log('Requisição concluída');
    //     },
    //   };

    //   this.authService.createLogin(this.username, this.password).subscribe(observer); 
    //} 
  }
}
