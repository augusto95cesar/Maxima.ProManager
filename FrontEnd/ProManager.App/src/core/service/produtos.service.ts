import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { API_CONFIG } from '../../config/api-config';
import { AuthService } from './authService';
import { Observable } from 'rxjs';
import { Produtos } from '../models/products.models';


@Injectable({
    providedIn: 'root'
})
export class ProdutoService {

    private baseUrl = API_CONFIG.BASE_URL;

    constructor(private http: HttpClient, private router: Router, private auth: AuthService) { }

    get(): Observable<Produtos[]> {
        var token = this.auth.getToken(); 
        const headers = new HttpHeaders({
            'Authorization': `${token}` // Adiciona o token de autenticação  
        });

        return this.http.get<Produtos[]>(`${this.baseUrl}/Produtos`, { headers });
    }

    post(produto : any): Observable<any> {
        var url = `${this.baseUrl}`  + '/Produtos';

        var token = this.auth.getToken();
        const headers = new HttpHeaders({
            Authorization: `${token}`,
            'Content-Type': 'application/json',
        });

        //console.log(produto)

        return this.http.post(url,  produto , { headers }); // Envia um corpo 
    }

    delete(codigo : any): Observable<any> {
        var url = `${this.baseUrl}`  + '/Produtos/' + codigo;

        var token = this.auth.getToken();
        const headers = new HttpHeaders({
            Authorization: `${token}`,
            'Content-Type': 'application/json',
        });

        //console.log(produto)

        return this.http.delete(url, { headers }); // Envia um corpo 
    }
}
