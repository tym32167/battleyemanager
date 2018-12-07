const localStorageMock = {
    clear: jest.fn(),
    getItem: jest.fn(),
    removeItem: jest.fn(),
    setItem: jest.fn(),
  };
  
const g: any = global
g.localStorage = localStorageMock;  
  