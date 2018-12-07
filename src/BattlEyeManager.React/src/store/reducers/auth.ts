import { authConstants } from '../constants';
import { StoreInitialState } from '../models';

export function authentication(state = StoreInitialState.auth, action: any) {
  switch (action.type) {
    case authConstants.LOGIN_REQUEST:
      return {
        loggingIn: true,
        user: action.user
      };
    case authConstants.LOGIN_SUCCESS:
      return {
        loggedIn: true,
        user: action.user
      };
    case authConstants.LOGIN_FAILURE:
      return {
        error: action.error
      };
    case authConstants.LOGOUT:
      return {};
    default:
      return state
  }
}