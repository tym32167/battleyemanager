import React, { Component } from 'react';
import { Route, Switch } from "react-router-dom";
import { NotFound } from '../404';
import { BanList } from './banlist';
import { DashBoard } from './dashboard';
import { List } from './list';
import { List as ChatList } from './onlineChat/list';
import { List as PlayersList } from './onlinePlayers/list';


export class OnlineServers extends Component {
    public render() {
        return (
            <React.Fragment>
                <Switch>
                    <DefaultLayout exact={true} path="/" component={List} />
                    <DefaultLayout exact={true} path="/online" component={List} />

                    <Route exact={true} path="/online/:serverId" component={DashBoard} />

                    <DefaultLayout exact={true} path="/online/:serverId/players" component={PlayersList} />
                    <Route exact={true} path="/online/:serverId/bans" component={BanList} />
                    <DefaultLayout exact={true} path="/online/:serverId/chat" component={ChatList} />

                    <DefaultLayout component={NotFound} />
                </Switch>
            </React.Fragment>
        );
    }
}

interface IDefaultLayoutProps {
    exact?: boolean,
    path?: string,
    component: any
}

const DefaultLayout = (props: IDefaultLayoutProps) => {
    const Comp = props.component;
    const { component, ...rest } = props;

    const rend = (matchProps: any) => (
        <React.Fragment>
            <div className="my-3 p-3 bg-white rounded box-shadow  col-12 col-lg-11 col-md-12">
                <Comp {...matchProps} />
            </div>
        </React.Fragment>
    );

    return (
        // tslint:disable-next-line:jsx-no-lambda
        <Route {...rest} render={rend} />
    )
};
