
#

## 📝 脚本使用说明

这两个脚本的作用是：**在 `./src/` 目录下自动生成一篇带有唯一 ID 的 Markdown 文章模板**，省去你手动创建文件、填写 Front Matter 的麻烦

### 特性

- 自动生成符合 UUID v4 规范的唯一文件名（如 `src/ab/cdef1234...md`）
- 自动创建二级目录（如 `src/ab/`）
- 自动填入当前日期（`create_at`）
- 自动获取 Git 用户名作为作者，若获取不到则回退为脚本名
- 日期（`create_at`）使用系统当前日期，格式为 `YYYY-MM-DD`

---

## 它是干嘛的？

在项目的 ./src/ 目录下，自动生成一篇带“身份证号”的 Markdown 笔记。

## 怎么用？

1. 给执行权限
   - bash: `chmod +x ./utils/new.sh`
   - pwsh:

     ```powershell
     $path = "./utils/new.ps1"
     $rule = [Security.AccessControl.FileSystemAccessRule]::new("$env:USERNAME", "ReadAndExecute", "Allow")
     Set-Acl $path (Get-Acl $path).AddAccessRule($rule)
     ```

2. 运行脚本
   - bash: `./utils/new.sh`
   - pwsh: `./utils/new.ps1`

   如果运气不好文件名冲突（极小概率），脚本会报错停止，不会覆盖你已有的文件。

3. 一键创建&打开
   - bash: `code "$(./utils/new.sh)"` # 换成你的 editor
   - pwsh: `./utils/new.ps1 && code $path`

---

### 🔧 自定义

- 如果你想修改默认的回退作者名，找到脚本中的 `$git_name` 字样，改成你想要的即可。（记得不要提交进 git 仓库）
