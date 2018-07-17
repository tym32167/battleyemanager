import {authHeader} from './auth.header';

export const userService = {
    getUsers,
    getUser
};


function getUsers(){
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch('/api/user', requestOptions).then(handleResponse);
}

function getUser(id){
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch('/api/user/'+id, requestOptions).then(handleResponse);
}

function handleResponse(response) {
    if (!response.ok) { 
        return Promise.reject(response.statusText);
    }
    return response.json();
}