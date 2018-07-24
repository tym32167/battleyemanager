import React, { Component } from 'react';
import { Route, Switch } from "react-router-dom";
import {List} from './list';
import {Create} from './create';

export class Servers extends Component {
    render() {
        return (
            <React.Fragment>
                <div className="my-3 p-3 bg-white rounded box-shadow  col-12 col-lg-8 col-md-10">
                    <Switch>
                    <Route exact path="/servers" component={List} />
                    <Route exact path="/servers/create" component={Create} />
                    </Switch>
                </div>
            </React.Fragment>
        );
    }
}
