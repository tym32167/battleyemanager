import React from 'react';

import { List as PlayersList } from './onlinePlayers/list';
import { List as ChatList } from './onlineChat/list';
import { ServerHeader} from './onlineServerHeader';

import './dashboard.css';

export const DashBoard = (props) => (
    <React.Fragment>
        <div className="container-fluid">
            <div className="row">
                <div className="col-12 " >
                    <div className="p-3 m-3 bg-white rounded box-shadow">
                        <ServerHeader {...props} />
                    </div>
                </div>
                
                <div className="col-sm-12 col-lg-5">
                    <div className="p-3 m-3 bg-white rounded box-shadow">
                        <ChatList {...props} />
                    </div>
                </div>

                <div className="col-sm-12 col-lg-7">
                    <div className="p-3 m-3 bg-white rounded box-shadow">
                        <PlayersList {...props} />
                    </div>
                </div>
            </div>
        </div>
    </React.Fragment>
);