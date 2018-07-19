import { history } from './history';
import {authService} from './auth.service';
import axios from 'axios';

axios.interceptors.request.use(function (config) {
    // Do something before request is sent
    const user = JSON.parse(localStorage.getItem('user'));
    if (user && user.token) {
        config.headers.common['Authorization'] = 'Bearer ' + user.token;        
    } 
    return config;
  }, function (error) {
    // Do something with request error
    return Promise.reject(error);
  });
 
// Add a response interceptor
axios.interceptors.response.use(function (response) {
    // Do something with response data
    return response;
  }, function (error) {
    if (error.response.status === 401) {
        authService.logout();
        history.push('/login');
    }
    // Do something with response error
    return Promise.reject(error);
  });