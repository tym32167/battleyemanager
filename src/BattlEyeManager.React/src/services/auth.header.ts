import axios from 'axios';
import { IAuthUserInfo } from 'src/models';
import { authService } from './auth.service';
import { history } from './history';

axios.interceptors.request.use((config) => {
  // Do something before request is sent    
  const user: IAuthUserInfo = JSON.parse(localStorage.getItem('user') || '{}');
  if (user && user.token) {
    config.headers.common.Authorization = 'Bearer ' + user.token;
  }
  return config;
}, (error) => {
  // Do something with request error
  return Promise.reject(error);
});

// Add a response interceptor
axios.interceptors.response.use((response) => {
  // Do something with response data
  return response;
}, (error) => {
  if (error.response && error.response.status === 401) {
    authService.logout();
    history.push('/login');
  }
  // Do something with response error
  return Promise.reject(error);
});