import React, { Component } from 'react';
import { Route } from "react-router-dom";
import { List } from './list';
import { Edit } from './edit';


export class Users extends Component {
    render() {
        return (
            <React.Fragment>
                <Route exact path="/users" component={List} />
                <Route exact path="/users/:id" component={Edit} />
            </React.Fragment>
        );
    }
}
