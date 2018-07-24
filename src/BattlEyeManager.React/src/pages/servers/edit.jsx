import React, { Component } from 'react';
import { connect } from "react-redux";
import { serverActions } from "../../store/actions";
import { EditForm } from './controls';
import {Error} from '../../controls';

class Edit extends Component {

    componentDidMount() {
        this.props.onLoad(this.props.id);
    }

    render() {
        const { item, onSubmit, error } = this.props;

        return (
            <React.Fragment>
                <h2>Edit Server</h2>
                <Error error={error} />
                {item && <EditForm onSubmit={onSubmit} initialValues={item} edit={true} />}                
            </React.Fragment>
        );
    }
}

const mapStateToProps = ({ servers }, ownProps) => {
    return {
        item: servers.item,
        id: ownProps.match.params.id,
        error: servers.error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: (id) => dispatch(serverActions.get(id)),
        onSubmit: (item) => dispatch(serverActions.update(item))
    }
}

const ConnectedEdit = connect(
    mapStateToProps,
    mapDispatchToProps
)(Edit);

export { ConnectedEdit as Edit };