import React from 'react';
import { connect } from 'react-redux';
import { Button } from 'reactstrap';
import { Dispatch } from 'redux';
import { ConfirmWindow } from 'src/controls';
import { IOnlineBan } from 'src/models';
import { onlineBanActions } from 'src/store/actions';

interface IRemoveBanProps {
    ban: IOnlineBan,
    onRemove: (ban: IOnlineBan) => void;
}

class RemoveBan extends React.Component<IRemoveBanProps> {
    public render() {
        const { ban, onRemove } = this.props;
        const submit = () => {
            onRemove(ban);
        };

        const renderer = (toggle: () => void) => <Button color="danger" size="sm" onClick={toggle} >Remove</Button>;

        return (
            <React.Fragment>
                <ConfirmWindow
                    submit={submit}
                    text={'Remove ban #' + ban.num + '?'}
                    title="Remove Ban"
                    buttonRenderer={renderer}
                />
            </React.Fragment>
        );
    }
}

const mapStateToProps = () => {
    return {

    }
}

const mapDispatchToProps = (dispatch: Dispatch<void>, props: any) => {
    return {
        onRemove: (ban: IOnlineBan) => {
            dispatch(onlineBanActions.removeBan(ban.serverId, ban.num));
        }
    }
}

const ConnectedRemoveBan = connect(
    mapStateToProps,
    mapDispatchToProps
)(RemoveBan);

export { ConnectedRemoveBan as RemoveBan };