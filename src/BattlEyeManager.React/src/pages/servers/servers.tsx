import React, { Component } from 'react';
import { Route, Switch } from "react-router-dom";
import { NotFound } from '../404';
import { Create } from './create';
import { Edit } from './edit';
import { List } from './list';
import { ServerScriptList } from './scripts';

export class Servers extends Component {
    public render() {
        return (
            <React.Fragment>
                <div className="my-3 p-3 bg-white rounded box-shadow  col-12 col-lg-10 col-md-10">
                    <Switch>
                        <Route exact={true} path="/servers" component={List} />
                        <Route exact={true} path="/servers/create" component={Create} />
                        <Route exact={true} path="/servers/:id" component={Edit} />
                        <Route exact={true} path="/servers/:serverId/scripts" component={ServerScriptList} />
                        <Route component={NotFound} />
                    </Switch>
                </div>
            </React.Fragment>
        );
    }
}
