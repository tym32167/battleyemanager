import React, { Component } from 'react';
import { Route, Switch } from "react-router-dom";
import { List } from './list';
import {NotFound} from '../404';
import {List as PlayersList} from './onlinePlayers/list';
import {List as ChatList} from './onlineChat/list';
import {DashBoard} from './dashboard';
import PropTypes from 'prop-types';


export class OnlineServers extends Component {
    render() {
        return (
            <React.Fragment>                
                    <Switch>
                        <DefaultLayout exact path="/" component={List} />
                        <DefaultLayout exact path="/online" component={List} />

                        <Route exact path="/online/:serverId" component={DashBoard} />

                        <DefaultLayout exact path="/online/:serverId/players" component={PlayersList} />
                        <DefaultLayout exact path="/online/:serverId/chat" component={ChatList} />                        

                        <DefaultLayout component={NotFound}/>
                    </Switch>                
            </React.Fragment>
        );
    }
}



const DefaultLayout = ({ component: Component, ...rest }) => {
    return (
      <Route {...rest} render={matchProps => (
        <div className="my-3 p-3 bg-white rounded box-shadow  col-12 col-lg-6 col-md-8">
            <Component {...matchProps} />
            </div>      
      )} />
    )
  };
  
  DefaultLayout.propTypes = {
    component: PropTypes.oneOfType([PropTypes.element, PropTypes.func]).isRequired
  }