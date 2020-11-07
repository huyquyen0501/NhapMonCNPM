import { NhapMonCNPMTemplatePage } from './app.po';

describe('NhapMonCNPM App', function() {
  let page: NhapMonCNPMTemplatePage;

  beforeEach(() => {
    page = new NhapMonCNPMTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
