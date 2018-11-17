import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import { IOnlineBan } from 'src/models';
import { onlineBanActions } from 'src/store/actions';
import { Error } from '../../controls';
import { ServerHeader } from './onlineServerHeader';


export const BanList = (props: any) => (
    <React.Fragment>
        <div className="container-fluid p-lg-3 p-1">
            <div className="row">
                <div className="col-12 p-2 m-0" >
                    <div className="bg-white rounded box-shadow  p-1">
                        <ServerHeader {...props} />
                    </div>
                </div>

                <div className="col-sm-12 col-lg-11  p-2 m-0">
                    <div className="bg-white rounded box-shadow p-1">
                        <ConnectedBanListTable {...props} />
                    </div>
                </div>
            </div>
        </div>
    </React.Fragment>
);

interface IBanListProps {
    onLoad: () => void,
    items: IOnlineBan[],
    error: any
}



class BanListTable extends Component<any> {
    public t: IBanListProps;
    public componentDidMount() {
        this.props.onLoad(this.props.serverId);
    }

    public render() {

        const { items, error } = this.props;
        const len = items ? items.length : 0;

        return (
            <React.Fragment>
                <h2>Online Bans ({len})</h2>
                <Error error={error} />
                {/* {items && <Serverstable items={items} />} */}
            </React.Fragment>
        );
    }
}

const mapStateToProps = ({ onlineBans }: { onlineBans: any }, ownProps: any) => {
    const server = onlineBans[ownProps.match.params.serverId];
    let items = [];
    if (server &&
        server.items) { items = server.items; }

    let error;
    if (server &&
        server.error) { error = server.error; }

    return {
        busy: server && server.busy,
        error,
        items,
        serverId: ownProps.match.params.serverId,
    }
}

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
        onLoad: (serverId: number) => {
            dispatch(onlineBanActions.getItems(serverId));
        }
    }
}

const ConnectedBanListTable = connect(
    mapStateToProps,
    mapDispatchToProps
)(BanListTable);
