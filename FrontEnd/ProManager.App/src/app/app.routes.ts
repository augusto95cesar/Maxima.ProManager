import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { ProdutoComponent } from './produto/listarProduto/produto.component';
import { AuthGuard } from '../core/guard/auth.guard';
import { CreateProdutoComponent } from './produto/createProduto/create.produto.component';


export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'produto', component: ProdutoComponent, canActivate: [AuthGuard] },
  { path: 'createproduto', component: CreateProdutoComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
]; 