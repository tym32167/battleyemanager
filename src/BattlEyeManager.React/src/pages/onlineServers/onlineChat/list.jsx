import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { onlineChatActions, onlineServerActions } from "../../../store/actions";
import { connect } from 'react-redux';
import { Error } from '../../../controls';
import PropTypes from 'prop-types';
import {Form, Input, Button} from 'reactstrap';

import './list.css';

class List extends Component {

    componentDidMount() {
        const { serverId } = this.props;
        this.props.onLoad(serverId);
    }
    render() {

        const { items, error, server, serverId } = this.props;
        const len = items.length;

        return (
            <React.Fragment>
                <h3>{server && server.name}</h3>
                <h4><Link to={'/online/' + serverId + '/players'}>Players</Link> {' '} Chat ({len})</h4>
                <Error error={error} />
                {items && <ItemsTable items={items} />}
                {/* <form className="form-inline">
                    <input type="text" className="form-control col-sm" id="chatMessage" name="chatMessage" placeholder="Chat message" />
                    <input type="submit" className="btn btn-primary" value="Send" />
                </form> */}

                <Form inline>
                    <Input className="col-sm" placeholder="Chat Message"></Input>
                    <Button color="primary">Send</Button>
                </Form>
            </React.Fragment>
        );
    }
}

class ItemsTable extends Component {

    constructor(props){
        super(props);

        this.chatBox = React.createRef();
    }

    componentDidUpdate(){       
            var height = this.chatBox.current.scrollHeight;
            this.chatBox.current.scrollTop = height;
    }

    render() {
        const { items } = this.props;
        return (<div id="chatBox" ref={this.chatBox} className="my-3 p-3 bg-white rounded box-shadow">
            {items.map((item, i) => <Item key={i} item={item} />)}
        </div>);
    }
}
function getClassname(messageType){
    switch(messageType)
    {
        case 'Vehicle':
        case 'Command':
            return "table-warning";
        case 'Group':
            return "table-success";
        case 'Side':
            return "table-info";
        case 'Direct':
            return "table-active";
        case 'RCon':
            return "table-danger";
        case 'Global':
            return '';
        default:
            return '';
    }
}

const Item = ({ item }) => (
    <div className="media text-muted @GetClass(item)">
        <p className={'media-body mb-0 small lh-125 ' + getClassname(item.type)}>
            <strong>{new Date(item.date).toLocaleTimeString()}{' '}</strong>
            {item.message}
        </p>
    </div>

)


const mapStateToProps = ({ onlineChat, onlineServers }, ownProps) => {
    const server = onlineChat[ownProps.match.params.serverId];
    let items = [];
    if (server &&
        server.items)
        items = server.items;

    let error = undefined;
    if (server &&
        server.error)
        error = server.error;

    return {
        serverId: ownProps.match.params.serverId,
        items: items,
        server: onlineServers.item,
        error: error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: (serverId) => {
            dispatch(onlineChatActions.getItems(serverId));
            dispatch(onlineServerActions.getItem(serverId));
        }
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(List);

export { ConnectedList as List };

List.propTypes = {
    onLoad: PropTypes.func.isRequired,
    items: PropTypes.array,
    server: PropTypes.object,
    serverId: PropTypes.oneOfType([PropTypes.number, PropTypes.string]),
    error: PropTypes.oneOfType([PropTypes.string, PropTypes.object])
}

ItemsTable.propTypes = {
    items: PropTypes.array
}

Item.propTypes = {
    item: PropTypes.object
}