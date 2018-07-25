import { serverConstants } from '../constants';
import {serverService, history} from '../../services';
import { onlineServerActions } from './onlineServer';

export const serverActions = {
    getAll,
    add,
    update, 
    del, 
    get
};

function getAll() {
    return dispatch => {
        dispatch(request([]));
        serverService.getAll()
            .then(
                items => {
                    dispatch(success(items));
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(items) { return { type: serverConstants.GET_SERVERS_REQUEST, items } }
    function success(items) { return { type: serverConstants.GET_SERVERS_SUCCESS, items } }
    function failure(error) { return { type: serverConstants.GET_SERVERS_FAILURE, error } }
}

function add(item) {
    return dispatch => {
        dispatch(request(item));
        serverService.add(item)
            .then(
                item => {
                    dispatch(success(item));
                    dispatch(onlineServerActions.getAll());
                    history.push('/servers');
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item)  { return { type: serverConstants.CREATE_SERVER_REQUEST, item } }
    function success(item)  { return { type: serverConstants.CREATE_SERVER_SUCCESS, item } }
    function failure(error) { return { type: serverConstants.CREATE_SERVER_FAILURE, error } }
}




function del(item) {
    return dispatch => {
        dispatch(request(item));
        serverService.del(item.id)
            .then(
                item => {
                    dispatch(success(item));
                    dispatch(getAll());
                    dispatch(onlineServerActions.getAll());
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item)  { return { type: serverConstants.DELETE_SERVER_REQUEST, item } }
    function success(item)  { return { type: serverConstants.DELETE_SERVER_SUCCESS, item } }
    function failure(error) { return { type: serverConstants.DELETE_SERVER_FAILURE, error } }
}

function update(item) {
    return dispatch => {
        dispatch(request(item));
        serverService.update(item)
            .then(
                item => {
                    dispatch(success(item));
                    dispatch(onlineServerActions.getAll());
                    history.push('/servers');
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item) { return { type: serverConstants.UPDATE_SERVER_REQUEST, item } }
    function success(item) { return { type: serverConstants.UPDATE_SERVER_SUCCESS, item } }
    function failure(error) { return { type: serverConstants.UPDATE_SERVER_FAILURE, error } }
}



function get(id) {
    return dispatch => {
        dispatch(request({id}));
        serverService.get(id)
            .then(
                item => {
                    dispatch(success(item));
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item) { return { type: serverConstants.GET_SERVER_BY_ID_REQUEST, item } }
    function success(item) { return { type: serverConstants.GET_SERVER_BY_ID_SUCCESS, item } }
    function failure(error) { return { type: serverConstants.GET_SERVER_BY_ID_FAILURE, error } }
}