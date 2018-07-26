import React, { Component } from 'react';
import { Route, Switch } from "react-router-dom";
import { List } from './list';
import {NotFound} from '../404';
import {List as PlayersList} from './onlinePlayers/list';
import {List as ChatList} from './onlineChat/list';


export class OnlineServers extends Component {
    render() {
        return (
            <React.Fragment>
                <div className="my-3 p-3 bg-white rounded box-shadow  col-12 col-lg-6 col-md-8">
                    <Switch>
                        <Route exact path="/" component={List} />
                        <Route exact path="/online" component={List} />

                        <Route exact path="/online/:serverId/players" component={PlayersList} />
                        <Route exact path="/online/:serverId/chat" component={ChatList} />

                        <Route component={NotFound}/>
                    </Switch>
                </div>
            </React.Fragment>
        );
    }
}
