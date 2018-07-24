import { serverConstants } from '../constants';
import {serverService} from '../../services';

export const serverActions = {
    getAll
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
