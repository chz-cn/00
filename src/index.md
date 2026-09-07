
<!-- markdownlint-disable -->
<style>
  :root {
    --card-bg-start: #f5f7fa;
    --card-bg-end: #c3cfe2;
    --card-text: #2d3436;
  }

  [data-bs-theme="dark"] {
    --card-bg-start: #4a5058;
    --card-bg-end: #2a2e34;
    --card-text: #dfe6e9;
  }

  [data-bs-theme="light"] {
    --card-bg-start: #f5f7fa;
    --card-bg-end: #c3cfe2;
    --card-text: #2d3436;
  }

  @media (prefers-color-scheme: dark) {

    :root:not([data-bs-theme="light"]):not([data-bs-theme="dark"]),
    [data-bs-theme="auto"] {
      --card-bg-start: #2d3436;
      --card-bg-end: #1a1a2e;
      --card-text: #dfe6e9;
    }
  }

  .empty-card {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100%;
    flex-direction: column;
    border-radius: 16px;
    padding: 40px;
    color: var(--card-text);
    background: linear-gradient(135deg, var(--card-bg-start), var(--card-bg-end));
  }

  .empty-icon {
    font-size: 72px;
    line-height: 1;
  }

  .empty-title {
    margin: 16px 0 8px;
    font-size: 28px;
    font-weight: 300;
  }
</style>

<div class="empty-card">
  <span class="empty-icon">📂</span>
  <h1 class="empty-title" id="folder">00/</h1>
</div>
