import React, { Component } from 'react';
import { Route, Switch } from "react-router-dom";
import { NotFound } from '../404';
import { Create } from './create';
import { Edit } from './edit';
import { List } from './list';


export class KickReasons extends Component {
    public render() {
        return (
            <React.Fragment>
                <div className="my-3 p-3 bg-white rounded box-shadow  col-12 col-lg-8 col-md-10">
                    <Switch>
                        <Route exact={true} path="/kickReasons" component={List} />
                        <Route exact={true} path="/kickReasons/create" component={Create} />
                        <Route exact={true} path="/kickReasons/:id" component={Edit} />
                        <Route component={NotFound} />
                    </Switch>
                </div>
            </React.Fragment>
        );
    }
}


