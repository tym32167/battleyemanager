import { applyMiddleware, createStore } from 'redux';
import { createLogger } from 'redux-logger';
import thunkMiddleware from 'redux-thunk';

import appReducer from './reducers';

const loggerMiddleware = createLogger();

export const store = createStore(
    appReducer,
    applyMiddleware(
        thunkMiddleware,
        loggerMiddleware
    )
);