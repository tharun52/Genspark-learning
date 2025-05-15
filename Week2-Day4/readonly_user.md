## ✅ PostgreSQL Read-Only User Setup (Port 5433)

### 1. **Start the PostgreSQL Server**

```bash
pg_ctl -D C:\pri -o "-p 5433" -l C:\pri\logfile start
```

---

### 2. **Open Server (connect to default `postgres` database)**

```bash
psql -p 5433 -d postgres
```

---

### 3. **Create sample database `dbsample`**

```sql
CREATE DATABASE dbsample;
```

---

### 4. **Open server with `dbsample`**

```bash
psql -p 5433 -d dbsample
```

---

### 5. **Create sample table**

```sql
CREATE TABLE sample(id INT, name TEXT);
```

---

### 6. **Insert two rows**

```sql
INSERT INTO sample VALUES (1, 'abc'), (2, 'def');
```

---

### 7. **Create user `readonly`**

```sql
CREATE ROLE readonly WITH LOGIN PASSWORD 'readonly_password';
```

---

### 8. **Grant connect on `dbsample`**

```sql
GRANT CONNECT ON DATABASE dbsample TO readonly;
```

---

### 9. **Grant schema usage and SELECT on all tables in `public`**

```sql
GRANT USAGE ON SCHEMA public TO readonly;
GRANT SELECT ON ALL TABLES IN SCHEMA public TO readonly;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO readonly;
```

---

### 10. **Exit**

```bash
\q
```

---

### 11. **Open `dbsample` with `readonly` user**

```bash
psql -p 5433 -d dbsample -U readonly
```

---

### 12. **Perform SELECT**

```sql
SELECT * FROM sample;
-- Output: (1, 'abc'), (2, 'def')
```

---

### 13. **Perform INSERT and show permission denied**

```sql
INSERT INTO sample VALUES (3, 'ghi');
-- ERROR: permission denied for table sample
```

---
