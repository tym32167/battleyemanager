import React from 'react';

import { List as ChatList } from './onlineChat/list';
import { List as PlayersList } from './onlinePlayers/list';
import { ServerHeader } from './onlineServerHeader';

import './dashboard.css';

export const DashBoard = (props: any) => (
    <React.Fragment>
        <div className="container-fluid p-lg-3 p-1">
            <div className="row">
                <div className="col-12 p-2 m-0" >
                    <div className="bg-white rounded box-shadow  p-1">
                        <ServerHeader {...props} />
                    </div>
                </div>

                <div className="col-sm-12 col-lg-5  p-2 m-0">
                    <div className="bg-white rounded box-shadow p-1">
                        <ChatList {...props} />
                    </div>
                </div>

                <div className="col-sm-12 col-lg-7  p-2 m-0">
                    <div className="bg-white rounded box-shadow p-1">
                        <PlayersList {...props} />
                    </div>
                </div>
            </div>
        </div>
    </React.Fragment>
);