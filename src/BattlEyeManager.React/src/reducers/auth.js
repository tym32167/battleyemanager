import { AUTH_LOGOUT, AUTH_LOGIN } from '../actions/auth'

export function logon(state = {}, action)
{
  switch(action.type)
  {
    case AUTH_LOGIN:
      return Object.assign({}, state, {user: action.user, token: action.token});
    case AUTH_LOGOUT:      
      var {user, token, newstate} = state;
      return newstate;
    default: 
      return state;
  }
}