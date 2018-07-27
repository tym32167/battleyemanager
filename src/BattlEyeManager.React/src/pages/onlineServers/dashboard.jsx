import React from 'react';

import { List as PlayersList } from './onlinePlayers/list';
import { List as ChatList } from './onlineChat/list';

import './dashboard.css';

export const DashBoard = (props) => (
    <React.Fragment>       

        <div className="card-columns online-server-dashboard p-3">

            <div className="card w-100" >
                <div className="card-body">
                    <h3>Server name</h3>
                </div>
            </div>

            <div className="card">
                <div className="card-body">
                    <PlayersList {...props} />
                </div>
            </div>
            <div className="card">
                <div className="card-body">
                    <ChatList {...props} />
                </div>
            </div>
        </div>
    </React.Fragment>
);