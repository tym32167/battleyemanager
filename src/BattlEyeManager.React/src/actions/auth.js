export const AUTH_LOGIN = 'AUTH_LOGIN';
export const AUTH_LOGOUT = 'AUTH_LOGOUT';

export function login(user, token) {
  return {
    type: AUTH_LOGIN,
    user,
    token
  }
}

export function logout() {
  return {
    type: AUTH_LOGOUT
  }
}