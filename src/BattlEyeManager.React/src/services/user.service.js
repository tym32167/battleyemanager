import {authHeader} from './auth.header';

export const userService = {
    getUsers
};


function getUsers(){
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch('/api/user', requestOptions).then(handleResponse);
}

function handleResponse(response) {
    if (!response.ok) { 
        return Promise.reject(response.statusText);
    }
    return response.json();
}