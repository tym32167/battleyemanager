import React, { Component } from 'react';
import { Route, Switch } from "react-router-dom";
import { List } from './list';
import { Create } from './create';
import { Edit } from './edit';
import { NotFound } from '../404';

export class Servers extends Component {
    render() {
        return (
            <React.Fragment>
                <div className="my-3 p-3 bg-white rounded box-shadow  col-12 col-lg-6 col-md-8">
                    <Switch>
                        <Route exact path="/servers" component={List} />
                        <Route exact path="/servers/create" component={Create} />
                        <Route exact path="/servers/:id" component={Edit} />
                        <Route component={NotFound} />
                    </Switch>
                </div>
            </React.Fragment>
        );
    }
}
