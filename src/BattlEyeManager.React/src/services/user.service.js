import axios from 'axios';

export const userService = {
    getUsers,
    getUser
};

function getUsers(){
    return axios.get('/api/user').then(response => response.data)
        .catch(error => Promise.reject(error.message));
}

function getUser(id){
    return axios.get('/api/user/'+id).then(response => response.data)
    .catch(error => Promise.reject(error.message));
}