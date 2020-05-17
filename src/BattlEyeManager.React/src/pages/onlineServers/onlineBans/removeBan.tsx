import i18next from 'i18next';
import React from 'react';
import { Trans } from 'react-i18next';
import { withTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import { Button } from 'reactstrap';
import { Action, Dispatch } from 'redux';
import { ConfirmWindow } from 'src/controls';
import { IOnlineBan } from 'src/models';
import { onlineBanActions } from 'src/store/actions';

interface IRemoveBanProps {
    ban: IOnlineBan,
    onRemove: (ban: IOnlineBan) => void;

    t: i18next.TFunction
}

class RemoveBan extends React.Component<IRemoveBanProps> {
    public render() {
        const { ban, onRemove, t } = this.props;
        const submit = () => {
            onRemove(ban);
        };

        const renderer = (toggle: () => void) => <Button color="danger" size="sm" onClick={toggle}><Trans>Remove</Trans></Button>;

        return (
            <React.Fragment>
                <ConfirmWindow
                    submit={submit}
                    text={t("Remove Ban") + ' #' + ban.num + '?'}
                    title={t("Remove Ban")}
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

const mapDispatchToProps = (dispatch: Dispatch<Action<string>>, props: any) => {
    return {
        onRemove: (ban: IOnlineBan) => {
            onlineBanActions.removeBan(ban.serverId, ban.num)(dispatch);
        }
    }
}

const ConnectedRemoveBan = connect(
    mapStateToProps,
    mapDispatchToProps
)(RemoveBan);

const Translated = withTranslation()(ConnectedRemoveBan);

export { Translated as RemoveBan };