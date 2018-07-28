import React, { Component } from 'react';
import { onlineChatActions } from "../../../store/actions";
import { connect } from 'react-redux';
import { Error, Chat } from '../../../controls';
import PropTypes from 'prop-types';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

class List extends Component {

    constructor(props){
        super(props);
        this.refresh = this.refresh.bind(this);
    }

    componentDidMount() {
        this.refresh();
    }

    refresh(){
        const { serverId } = this.props;
        this.props.onLoad(serverId);
    }

    render() {

        const { items, error, busy } = this.props;
        const len = items.length;

        return (
            <React.Fragment>                
                <h4> <small><FontAwesomeIcon icon="sync" onClick={(e) => this.refresh()} /></small> Chat ({len}) {busy && <small>loading....</small>} </h4>                                                
                <Error error={error} />
                {items && <Chat items={items} />}
            </React.Fragment>
        );
    }
}

const mapStateToProps = ({ onlineChat }, ownProps) => {    

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
        error: error,
        busy: onlineChat.busy
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: (serverId) => {
            dispatch(onlineChatActions.getItems(serverId));
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
    serverId: PropTypes.oneOfType([PropTypes.number, PropTypes.string]),
    error: PropTypes.oneOfType([PropTypes.string, PropTypes.object]),
    busy : PropTypes.bool
}

