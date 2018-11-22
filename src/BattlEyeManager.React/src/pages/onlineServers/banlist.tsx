import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import { IOnlineBan, IOnlineServer } from 'src/models';
import { onlineBanActions } from 'src/store/actions';
import { BootstrapTable, Error, FilterControl, IBootstrapTableColumn, IFilterControlProps, IPagerControlProps, PagerControl } from '../../controls';
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
    onLoad: (serverId: number) => void,
    items: IOnlineBan[],
    serverId: number,
    error: any
}



class BanListTable extends Component<IBanListProps> {
    public t: IBanListProps;
    public componentDidMount() {
        this.props.onLoad(this.props.serverId);
    }

    public render() {

        const { items, error } = this.props;
        const len = items ? items.length : 0;

        const columns: Array<IBootstrapTableColumn<IOnlineBan>> = [
            { header: "Num", renderer: row => row.num },
            { header: "Minutes left", renderer: row => row.minutesleft },
            { header: "Reason", renderer: row => row.reason },
            { header: "Guid or IP", renderer: row => row.guidIp },
        ];


        const filterProps: IFilterControlProps<IOnlineBan> = {
            children: (props) => {
                const pagerProps: IPagerControlProps<IOnlineServer> = {
                    ...props,
                    children: (p) => <BootstrapTable columns={columns} {...p} />,
                    pageSize: 10,
                }
                return (<PagerControl {...pagerProps} />);
            },
            data: items,
        }

        return (
            <React.Fragment>
                <h2>Online Bans ({len})</h2>
                <Error error={error} />
                {items &&
                    <React.Fragment>
                        <FilterControl {...filterProps} />
                    </React.Fragment>}
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
