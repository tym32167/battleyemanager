import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';
import { Dispatch } from 'redux';
import { IOnlineServer } from 'src/models';
import { BootstrapTable, Error, FilterControl, IBootstrapTableColumn, IFilterControlProps } from '../../controls';
import { onlineServerActions } from "../../store/actions";


interface IListProps {
    onLoad: () => void,
    error: any,
    items: IOnlineServer[]
}


class List extends Component<IListProps> {

    public componentDidMount() {
        this.props.onLoad();
    }

    public render() {

        const { items, error } = this.props;
        const len = items.length;

        const boolRender = (val: boolean) => <input type="checkbox" disabled={true} checked={val} />

        const columns: Array<IBootstrapTableColumn<IOnlineServer>> = [
            { header: "Name", renderer: row => row.name, rowStyle: { whiteSpace: "nowrap" } },
            { header: "Host", renderer: row => row.host, headerStyle: { width: "1%" } },
            { header: "Port", renderer: row => row.port, headerStyle: { width: "1%" } },
            { header: "Active", renderer: row => boolRender(row.active), headerStyle: { width: "1%" } },
            { header: "Connected", renderer: row => boolRender(row.isConnected), headerStyle: { width: "1%" } },
            { header: "Players", renderer: row => row.playersCount, headerStyle: { width: "1%" } },
            { header: "Bans", renderer: row => row.bansCount, headerStyle: { width: "1%" } },
            { header: "Admins", renderer: row => row.adminsCount, headerStyle: { width: "1%" } },
            {
                header: "",
                headerStyle: { width: "1%" },
                renderer: row => (<Button color="success" to={'/online/' + row.id} tag={Link} size="sm">Dashboard</Button>),
            }
        ];

        const filterProps: IFilterControlProps<IOnlineServer> = {
            children: (props) => <BootstrapTable columns={columns} {...props} />,
            data: items
        }

        return (
            <React.Fragment>
                <h2>Online Servers ({len})</h2>
                <Error error={error} />
                {items &&
                    <React.Fragment>
                        <FilterControl {...filterProps} />
                    </React.Fragment>
                }
            </React.Fragment>
        );
    }
}
const mapStateToProps = ({ onlineServers }: { onlineServers: any }) => {
    return {
        error: onlineServers.error,
        items: onlineServers.items || [],
    }
}

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
        onLoad: () => dispatch(onlineServerActions.getItems())
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(List);

export { ConnectedList as List };