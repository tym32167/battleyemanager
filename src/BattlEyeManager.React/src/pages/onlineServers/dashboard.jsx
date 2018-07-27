import React from 'react';

import { List as PlayersList } from './onlinePlayers/list';
import { List as ChatList } from './onlineChat/list';

export const DashBoard = (props) => (
    <React.Fragment>
        {/* <div className="row">
            <div className="my-3 p-3 bg-white rounded box-shadow  col-6 card">
                <div className="card-bo">
                    <PlayersList {...props} />
                </div>
            </div>
            <div className="my-3 p-3 bg-white rounded box-shadow  col-6 card">
                <div className="card-bo">
                    <ChatList {...props} />
                </div>
            </div>
        </div> */}

        <div className="card-group">
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