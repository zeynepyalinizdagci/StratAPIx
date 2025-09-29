# StratAPIx
StratAPIx is light version of postman, dredd

# Execution Flow (TBD)
1. Frontend uploads OpenAPI spec → API parses → returns TestCase list.

2. User triggers a test run → RunController calls TestExecutor.

3. TestExecutor resolves each TestCase's AuthType via Strategy Factory.

4. Command Factory builds HTTP commands (GetCommand, PostCommand, etc.).

5. Execution Strategies handle auth-specific request execution.

6. Commands run in parallel using Parallel.ForEachAsync.

7. HttpClientHandler may use:

   * Proxy for routing requests.

   * Rate limiter to throttle requests.

8. Results are returned to frontend and persisted in the database.

9. Optional SignalR or polling updates frontend in real-time.

# Optional Features

* Rate Limiter: control max requests per API host.

* Proxy Support: configure per TestCase or globally.

* Real-time updates: SignalR or polling.

* Retry & Logging: Decorator pattern for enhanced reliability.