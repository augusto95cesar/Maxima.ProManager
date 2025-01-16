import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { API_CONFIG } from '../../config/api-config';
import { AuthService } from './authService';
import { Observable } from 'rxjs';
import { Departamentos } from '../models/Departamentos.models';


@Injectable({
    providedIn: 'root'
})
export class DepartamentoService {

    private baseUrl = API_CONFIG.BASE_URL;

    constructor(private http: HttpClient, private router: Router, private auth: AuthService) { }

    get(): Observable<Departamentos[]> {
        var token = this.auth.getToken(); 
        const headers = new HttpHeaders({
            'Authorization': `${token}` // Adiciona o token de autenticação  
        });

        return this.http.get<Departamentos[]>(`${this.baseUrl}/Departamentos`, { headers });
    } 
}
