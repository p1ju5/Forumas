import { useRef, useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'

import { Toast } from 'primereact/toast';

import appState from './appState';

import './App.scss';

import * as React from 'react';

class State {
	isInitialized: boolean = false;

	/**
	 * Makes a shallow clone. Use this to return new state instance from state updates.
	 * @returns A shallow clone of this instance.
	 */
	shallowClone(): State {
		return Object.assign(new State(), this);
	}
}


/**
 * Application. React component.
 * @returns Component HTML.
 */
function App() {
	//get state container and state updater
	const [state, setState] = useState(new State());

	//get ref to interact with the toast
	const toastRef = useRef<Toast>(null);


	/**
	 * This is used to update state without the need to return new state instance explicitly.
	 * It also allows updating state in one liners, i.e., 'update(state => state.xxx = yyy)'.
	 * @param updater State updater function.
	 */
	let update = (updater: () => void) => {
		updater();
		setState(state.shallowClone());
	}

	let updateState = (updater: (state: State) => void) => {
		setState(state => {
			updater(state);
			return state.shallowClone();
		})
	}

	//initialize
	if (!state.isInitialized) {
		//subscribe to app state changes
		appState.when(appState.isLoggedIn, () => {
			//this will force component re-rendering
			updateState(state => { });
		});

		//subscribe to user messages
		appState.msgs.subscribe(msg => {
			update(() => toastRef.current?.show(msg));
		});

		//indicate initialization is done
		updateState(state => state.isInitialized = true);
	}

	//render component HTML
	let html =
		<Router>
			<Toast ref={toastRef} position="top-right" />
		</Router>;

	//
	return html;
}

export default App;
