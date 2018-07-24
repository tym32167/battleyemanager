import { serverConstants } from '../constants';
import {serverService, history} from '../../services';

export const serverActions = {
    getAll,
    add
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
                user => {
                    dispatch(success(user));
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
