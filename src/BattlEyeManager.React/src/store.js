import { createStore, applyMiddleware } from 'redux';
import thunkMiddleware from 'redux-thunk';
import { createLogger } from 'redux-logger';

import appReducer from './reducers';


// const store = createStore(appReducer);

// export default store;


const loggerMiddleware = createLogger();

export const store = createStore(
    appReducer,
    applyMiddleware(
        thunkMiddleware,
        loggerMiddleware
    )
);