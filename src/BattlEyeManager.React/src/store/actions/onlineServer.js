import { onlineServerConstants } from '../constants';
import {onlineServerService} from '../../services';

export const onlineServerActions = {
    getAll,
    get
};

function getAll() {
    return dispatch => {
        dispatch(request([]));
        onlineServerService.getAll()
            .then(
                items => {
                    dispatch(success(items));
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(items) { return { type: onlineServerConstants.GET_ONLINE_SERVERS_REQUEST, items } }
    function success(items) { return { type: onlineServerConstants.GET_ONLINE_SERVERS_SUCCESS, items } }
    function failure(error) { return { type: onlineServerConstants.GET_ONLINE_SERVERS_FAILURE, error } }
}


function get(id) {
    return dispatch => {
        dispatch(request({id}));
        onlineServerService.get(id)
            .then(
                item => {
                    dispatch(success(item));
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item) { return { type: onlineServerConstants.GET_ONLINE_SERVER_BY_ID_REQUEST, item } }
    function success(item) { return { type: onlineServerConstants.GET_ONLINE_SERVER_BY_ID_SUCCESS, item } }
    function failure(error) { return { type: onlineServerConstants.GET_ONLINE_SERVER_BY_ID_FAILURE, error } }
}