import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';

const localStorageMock = {
  clear: jest.fn(),
  getItem: jest.fn(),
  removeItem: jest.fn(),
  setItem: jest.fn(),
};

const g: any = global
g.localStorage = localStorageMock;

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<App />, div);
  ReactDOM.unmountComponentAtNode(div);
});


it("sample test", () => {
  expect(1 + 1).toBe(1 + 1);
});