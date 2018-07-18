import { history } from './history';
import {authService} from './auth.service';

export function authHeader() {
    // return authorization header with jwt token
    let user = JSON.parse(localStorage.getItem('user'));

    if (user && user.token) {
        return { 'Authorization': 'Bearer ' + user.token };
    } else {
        return {};
    }
}

export function authGuard(response){
    if(response.status === 401){
        authService.logout();
        history.push('/login');
    }
}