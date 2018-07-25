import React, { Component } from 'react';
import { Route, Switch } from "react-router-dom";
import { List } from './list';
import {NotFound} from '../404';


export class OnlineServers extends Component {
    render() {
        return (
            <React.Fragment>
                <div className="my-3 p-3 bg-white rounded box-shadow  col-12 col-lg-6 col-md-8">
                    <Switch>
                        <Route exact path="/" component={List} />
                        <Route exact path="/online" component={List} />
                        <Route component={NotFound}/>
                    </Switch>
                </div>
            </React.Fragment>
        );
    }
}
