import React, { Component } from 'react';
import { Route } from "react-router-dom";
import { List } from './list';
import { Edit } from './edit';


export class Users extends Component {
    render() {
        return (
            <React.Fragment>
                <div className="my-3 p-3 bg-white rounded box-shadow  col-12 col-lg-8 col-md-10">
                    <Route exact path="/users" component={List} />
                    <Route exact path="/users/:id" component={Edit} />
                </div>
            </React.Fragment>
        );
    }
}
