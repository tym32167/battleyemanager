import React, { Component } from 'react';
import { Route, Switch } from "react-router-dom";
import { NotFound } from '../404';
import { ServerModeratorList } from './controls/serverModeratorList';
import { Create } from './create';
import { Edit } from './edit';
import { List } from './list';

export class Users extends Component {
    public render() {
        return (
            <React.Fragment>
                <div className="my-3 p-3 bg-white rounded box-shadow  col-12 col-lg-12 col-md-12">
                    <Switch>
                        <Route exact={true} path="/users" component={List} />
                        <Route exact={true} path="/users/create" component={Create} />
                        <Route exact={true} path="/users/:id" component={Edit} />
                        <Route exact={true} path="/users/visibleServers/:id" component={ServerModeratorList} />
                        <Route component={NotFound} />
                    </Switch>
                </div>
            </React.Fragment>
        );
    }
}
